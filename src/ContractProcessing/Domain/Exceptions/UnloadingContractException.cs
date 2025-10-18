namespace Warehouse.ContractProcessing.Domain.Exceptions
{
    /// <summary>
    /// Специальное исключение доменного уровня для операций над UnloadingContract.
    /// </summary>
    /// <param name="message"></param>
    public sealed class UnloadingContractException(string message) : Exception(message)
    {
    }
}
