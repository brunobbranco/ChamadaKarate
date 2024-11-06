namespace ChamadaKarate.core.BLL
{
    public class AulasAlunosCreateRequest
    {
        public DateTime data_aula { get; set; }
        public int id_aluno { get; set; }
    }
    public class AulasAlunosDeleteRequest
    {
        public DateTime data_aula { get; set; }
        public int id_aluno { get; set; }
    }
    public class AulasAlunos
    {
        public int id { get; set; }
        public DateTime data_aula { get; set; }
        public int id_aluno { get; set; }
        public string nome_aluno { get; set; }
        public int faixa_aluno { get; set; }

        public async Task<AulasAlunos> cadastraAulaAlunoAsync(core.BLL.AulasAlunosCreateRequest aula)
        {
            DAL.AulasAlunos instance = new DAL.AulasAlunos();

            if(!await verificaAlunoNaAula(aula.data_aula, aula.id_aluno))
            {
                BLL.Alunos dadosAluno = new Alunos();
                await dadosAluno.retornaAlunoPorId(aula.id_aluno);

                try
                {
                    await instance.cadastraAulasAlunoAsync(aula);
                }
                catch (Exception)
                {
                    throw;
                }

                AulasAlunos aulasAlunos = new AulasAlunos();
                aulasAlunos.id_aluno = dadosAluno.id;
                aulasAlunos.data_aula = aula.data_aula;
                aulasAlunos.nome_aluno = dadosAluno.nome;
                aulasAlunos.faixa_aluno = dadosAluno.faixa;
                return aulasAlunos;
            }
            return null;
        }

        public async Task<List<BLL.AulasAlunos>> retornaTodosAulas()
        {
            DAL.AulasAlunos instance = new DAL.AulasAlunos();
            return await instance.retornaTodosAulas();
        }

        public async Task<List<BLL.AulasAlunos>> retornaAulasAlunosPorDataAula(DateTime _data_aula)
        {
            DAL.AulasAlunos instance = new DAL.AulasAlunos();
            return await instance.retornaAulasAlunosPorDataAula(_data_aula);
        }

        public async Task<bool> excluiAulasAlunoAsync(core.BLL.AulasAlunosDeleteRequest aluno)
        {
            DAL.AulasAlunos instance = new DAL.AulasAlunos();
            return await instance.excluiAulasAlunoAsync(aluno);
        }

        private async Task<bool> verificaAlunoNaAula(DateTime data_aula, int id_aluno)
        {
            DAL.AulasAlunos aulasAlunos = new DAL.AulasAlunos();
            return await aulasAlunos.verificaAlunoNaAula(data_aula, id_aluno);
        }
    }
}
