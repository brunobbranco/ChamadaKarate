namespace ChamadaKarate.core.BLL
{
    public class Alunos
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int faixa { get; set; }
        public int idade { get; set; }
        public int ativo { get; set; }

        public async Task<int> cadastraAlunoAsync(core.BLL.Alunos aluno)
        {
            DAL.Alunos instance = new DAL.Alunos();
            return await instance.cadastraAlunoAsync(aluno);
        }

        public async Task<List<BLL.Alunos>> retornaTodosAlunos(int _ativo = 1)
        {
            DAL.Alunos instance = new DAL.Alunos();
            return await instance.retornaTodosAlunos(_ativo);
        }

        public async Task<BLL.Alunos> retornaAlunoPorId(int id_aluno)
        {
            DAL.Alunos instance = new DAL.Alunos();
            return await instance.retornaAlunoPorId(id_aluno);
        }

        public async Task<bool> atualizaAlunoAsync(core.BLL.Alunos aluno)
        {
            DAL.Alunos instance = new DAL.Alunos();
            return await instance.atualizaAlunoAsync(aluno);
        }
    }
}
