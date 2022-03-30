namespace RevendaVeiculos.Service.Base
{
    public class ServiceResult<T> where T : class
    {
        public ServiceResult()
        {
            Erros = new Dictionary<string, string>();
        }
        public ServiceResult(T result, IDictionary<string, string> erros)
        {
            Result = result;
            Erros = erros;
        }

        public ServiceResult(T result, StatusResultEnum status)
        {
            Result = result;
            Erros = new Dictionary<string, string>();
        }

        public T? Result { get; set; }
        public IDictionary<string, string> Erros { get; set; }
        public bool PossueErro
        {
            get
            {
                if (Erros.Count > 0)
                    return true;

                return false;
            }
        }
    }

    public enum StatusResultEnum
    {
        Erro = 0,
        Sucesso = 1
    }
}
