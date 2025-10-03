namespace SP.Aplicacao.DTOs.Common
{
    public class ResultadoDto<T>
    {
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public T? Dados { get; set; }
        public List<string> Erros { get; set; } = [];

        public static ResultadoDto<T> ComSucesso(T dados, string? mensagem = null)
        {
            return new ResultadoDto<T>
            {
                Sucesso = true,
                Dados = dados,
                Mensagem = mensagem
            };
        }

        public static ResultadoDto<T> ComErro(string erro)
        {
            return new ResultadoDto<T>
            {
                Sucesso = false,
                Erros = [erro]
            };
        }

        public static ResultadoDto<T> ComErros(List<string> erros)
        {
            return new ResultadoDto<T>
            {
                Sucesso = false,
                Erros = erros
            };
        }
    }

    public class ResultadoDto : ResultadoDto<object>
    {
        public static ResultadoDto ComSucesso(string? mensagem = null)
        {
            return new ResultadoDto
            {
                Sucesso = true,
                Mensagem = mensagem
            };
        }

        public new static ResultadoDto ComErro(string erro)
        {
            return new ResultadoDto
            {
                Sucesso = false,
                Erros = [erro]
            };
        }

        public new static ResultadoDto ComErros(List<string> erros)
        {
            return new ResultadoDto
            {
                Sucesso = false,
                Erros = erros
            };
        }
    }
}
