using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Utilities.DbContextSettings.Abstracts;
using Utilities.DbContextSettings.Transactionals;
using Warehouse.ContractProcessing.Applications.AppServices.Abstract;
using Warehouse.ContractProcessing.Domain.Aggregates;
using Warehouse.ContractProcessing.Domain.Exceptions;
using Warehouse.ContractProcessing.Domain.ValueObjects;

namespace Warehouse.ContractProcessing.Applications.AppServices
{
    /// <inheritdoc/>
    public sealed class UnloadingContractService(
        IRepository<UnloadingContract> contracts,
        ITransactionalExecutor transactional) : IUnloadingContractService
    {
        private readonly IRepository<UnloadingContract> _contracts = contracts;
        private readonly ITransactionalExecutor _transactional = transactional;

        /// <inheritdoc/>
        public async Task CreateContractAsync(
            [NotNull] Guid contractGuid,
            [NotNull] Guid warehouseGuid,
            [NotNull] Guid managerGuid,
            [NotNull] DateTime scheduledFor,
            [NotNull] CancellationToken cancellationToken)
        {
            await _transactional.StartEffect(async token =>
            {
                var contract = UnloadingContract.Create(
                    contractGuid,
                    warehouseGuid,
                    managerGuid,
                    scheduledFor);

                await _contracts.AddAsync(contract, token);
            }, IsolationLevel.Serializable, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task StartContractAsync([NotNull] Guid contractId, [NotNull] CancellationToken cancellationToken)
        {
            var contract = await FindContractAsync(contractId, cancellationToken);
            contract.Start();
            //Вызвался внешний сервис

            await _contracts.UpdateAsync(contract, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task CompleteContractAsync(
            [NotNull] Guid contractId,
            [NotNull] CancellationToken cancellationToken)
        {
            var contract = await FindContractAsync(contractId, cancellationToken);
            contract.Complete();
            await _contracts.UpdateAsync(contract, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task CancelContractAsync(
            [NotNull] Guid contractId,
            [NotNull] CancellationToken cancellationToken)
        {
            var contract = await FindContractAsync(contractId, cancellationToken);
            contract.Cancel();
            await _contracts.UpdateAsync(contract, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task AddLineToContractAsync(
            [NotNull] Guid contractId,
            [NotNull] Guid productId,
            [NotNull] int quantity,
            [NotNull] CancellationToken cancellationToken)
        {
            var id = ContractId.Of(contractId);

            var contract = await _contracts
                .Where(x => x.Id == id)
                .Include(c => c.Lines)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new UnloadingContractException("Контракт не найден.");

            contract.AddLine(productId, quantity);

            await _contracts.UpdateAsync(contract, cancellationToken);
        }

        /// <inheritdoc/>
        [return: NotNull]
        public async Task<UnloadingContract> GetContractSnapshotAsync(
            [NotNull] Guid contractId,
            [NotNull] CancellationToken cancellationToken)
        {
            var id = ContractId.Of(contractId);

            var contract = await _contracts
                .Where(x => x.Id == id)
                .Include(x => x.Lines)
                .SingleOrDefaultAsync(cancellationToken)
                ?? throw new UnloadingContractException("Контракт не найден.");

            return contract;
        }

        private async Task<UnloadingContract> FindContractAsync(Guid id, CancellationToken cancellationToken)
        {
            var contractId = ContractId.Of(id);

            var contract = await _contracts
                .Where(c => c.Id == contractId)
                .Include(c => c.Lines)
                .SingleOrDefaultAsync(cancellationToken) ?? throw new UnloadingContractException("Контракт не найден.");
            return contract;
        }
    }
}
