namespace ChamadaKarate.Client.Models
{
    public class Alunos
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int faixa { get; set; }
        public int idade { get; set; }
        public int ativo { get; set; }
        public bool clicado { get; set; }
    }

    public class AlunosResponse
    {
        public string id_aluno { get; set; }
    }

    public class AulasAlunos
    {
        public int id { get; set; }
        public DateTime? data_aula { get; set; }
        public int id_aluno { get; set; }
        public string nome_aluno { get; set; }
        public int faixa_aluno { get; set; }
        public bool clicado { get; set; }
    }

    public class AulasAlunosResponse
    {
        public string id_aula { get; set; }
    }

    public class Aulas
    {
        public int id { get; set; }
        public DateTime data_aula { get; set; }
    }
}