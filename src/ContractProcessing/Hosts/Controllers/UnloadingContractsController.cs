using Microsoft.AspNetCore.Mvc;
using Warehouse.ContractProcessing.Applications.Handlers.Command.AddUnloadingLine;
using Warehouse.ContractProcessing.Applications.Handlers.Command.CancelUnloadingContract;
using Warehouse.ContractProcessing.Applications.Handlers.Command.CompleteUnloadingContract;
using Warehouse.ContractProcessing.Applications.Handlers.Command.CreateUnloadingContract;
using Warehouse.ContractProcessing.Applications.Handlers.Command.StartUnloadingContact;
using Warehouse.ContractProcessing.Сontract.Request;
using Wolverine;

namespace Warehouse.ContractProcessing.Host.Public.Controllers
{
    /// <summary>
    /// Контроллер управления контрактами выгрузки.
    /// </summary>
    [ApiController]
    [Route("unloading-contracts")]
    public class UnloadingContractsController(IMessageBus bus) : ControllerBase
    {
        /// <summary>
        /// Шина сообщений для отправки команд.
        /// </summary>
        private readonly IMessageBus _bus = bus;

        /// <summary>
        /// Создаёт новый договор разгрузки.
        /// </summary>
        /// <param name="request">Запрос на создание договора разгрузки.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUnloadingContractRequest request)
        {
            var command = new CreateUnloadingContractCommand(
                request.ContractGuid,
                request.WarehouseGuid,
                request.ManagerGuid,
                request.ScheduledFor);

            await _bus.InvokeAsync(command);
            return NoContent();
        }

        /// <summary>
        /// Запускает договор разгрузки.
        /// </summary>
        [HttpPost("start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Start([FromBody] StartUnloadingContractRequest request)
        {
            var command = new StartUnloadingContractCommand(request.ContractId);
            await _bus.InvokeAsync(command);
            return Ok();
        }

        /// <summary>
        /// Завершает договор разгрузки.
        /// </summary>
        [HttpPost("complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Complete([FromBody] CompleteUnloadingContractRequest request)
        {
            var command = new CompleteUnloadingContractCommand(request.ContractId);
            await _bus.InvokeAsync(command);
            return Ok();
        }

        /// <summary>
        /// Отменяет договор разгрузки.
        /// </summary>
        [HttpPost("cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cancel([FromBody] CancelUnloadingContractRequest request)
        {
            var command = new CancelUnloadingContractCommand(request.ContractId);
            await _bus.InvokeAsync(command);
            return Ok();
        }

        /// <summary>
        /// Добавляет товарную позицию в договор разгрузки.
        /// </summary>
        [HttpPost("lines")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddLine([FromBody] AddUnloadingLineRequest request)
        {
            var command = new AddUnloadingLineCommand(request.ContractId, request.ProductId, request.Quantity);
            await _bus.InvokeAsync(command);
            return Ok();
        }
    }
}
