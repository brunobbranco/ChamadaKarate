using ChamadaKarate.core.BLL;
using Npgsql;
using System.Configuration;

namespace ChamadaKarate.DAL
{
    public class AulasAlunos
    {
        public async Task<int> cadastraAulasAlunoAsync(core.BLL.AulasAlunosCreateRequest aula)
        {
            using (var conn = new NpgsqlConnection(core.BLL.Configuracao.getStrConnection("PRD")))
            {
                try
                {
                    conn.Open();
                    string strCommand = "insert into \"aluno_aula\" ( data_aula, id_aluno) values (@p1,@p2) RETURNING ID";
                    var command = new NpgsqlCommand(strCommand, conn);
                    command.Parameters.AddWithValue("@p1", aula.data_aula);
                    command.Parameters.AddWithValue("@p2", aula.id_aluno);
                    object? retorno = await command.ExecuteScalarAsync();

                    conn.Close();
                    return Convert.ToInt32(retorno);
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
        }

        public async Task<List<core.BLL.AulasAlunos>> retornaTodosAulas()
        {
            var result = new List<core.BLL.AulasAlunos>();

            using (var connection = new NpgsqlConnection(Configuracao.getStrConnection("PRD")))
            {
                connection.Open();

                string strCommand = "";
                strCommand += "SELECT ";
                strCommand += "public.aluno_aula.id "; //0
                strCommand += ", public.aluno_aula.data_aula "; //1
                strCommand += "FROM ";
                strCommand += "public.aluno_aula ";
                strCommand += "WHERE 1=1 ";                
                strCommand += "ORDER BY public.aluno_aula.data_aula DESC ";

                using (var command = new NpgsqlCommand(strCommand, connection))
                {
                    //command.Parameters.AddWithValue("@p1", _ativo);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Lê os dados e adiciona à lista
                        while (reader.Read())
                        {
                            var row = new core.BLL.AulasAlunos
                            {
                                id = reader.GetInt32(0),
                                data_aula = reader.GetDateTime(1),
                            };
                            result.Add(row);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<List<core.BLL.AulasAlunos>> retornaAulasAlunosPorDataAula(DateTime _data_aula)
        {
            var result = new List<core.BLL.AulasAlunos>();

            using (var connection = new NpgsqlConnection(Configuracao.getStrConnection("PRD")))
            {
                connection.Open();

                string strCommand = "";
                strCommand += "SELECT ";
                strCommand += "public.aluno_aula.id "; //0
                strCommand += ", date(public.aluno_aula.data_aula) data_aula "; //1
                strCommand += ", public.aluno_aula.id_aluno "; //2
                strCommand += ", public.alunos.nome "; //3
                strCommand += ", public.alunos.faixa "; //4
                strCommand += "FROM ";
                strCommand += "public.aluno_aula join public.alunos on ";
                strCommand += "public.aluno_aula.id_aluno = public.alunos.id ";
                strCommand += "WHERE 1=1 ";
                strCommand += "AND date(public.aluno_aula.data_aula) = @p0 ";
                strCommand += "ORDER BY public.aluno_aula.data_aula DESC ";

                using (var command = new NpgsqlCommand(strCommand, connection))
                {
                    command.Parameters.AddWithValue("@p0", _data_aula);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Lê os dados e adiciona à lista
                        while (reader.Read())
                        {
                            var row = new core.BLL.AulasAlunos
                            {
                                id = reader.GetInt32(0),
                                data_aula = reader.GetDateTime(1),
                                id_aluno = reader.GetInt32(2),
                                nome_aluno = reader.GetString(3),
                                faixa_aluno = reader.GetInt32(4)
                            };
                            result.Add(row);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<bool> excluiAulasAlunoAsync(core.BLL.AulasAlunosDeleteRequest aluno)
        {
            using (var conn = new NpgsqlConnection(core.BLL.Configuracao.getStrConnection("PRD")))
            {
                try
                {
                    conn.Open();
                    string strCommand = "delete from public.aluno_aula where date(data_aula) = @p0 and id_aluno = @p1";
                    var command = new NpgsqlCommand(strCommand, conn);
                    command.Parameters.AddWithValue("@p0", aluno.data_aula);
                    command.Parameters.AddWithValue("@p1", aluno.id_aluno);
                    int retorno = await command.ExecuteNonQueryAsync();

                    if (retorno > 0)
                    {
                        return true;
                    }
                    conn.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<bool> verificaAlunoNaAula(DateTime data_aula, int id_aluno)
        {
            using (var connection = new NpgsqlConnection(Configuracao.getStrConnection("PRD")))
            {
                connection.Open();

                string strCommand = "";
                strCommand += "SELECT ";
                strCommand += "public.aluno_aula.* "; //0
                strCommand += "FROM ";
                strCommand += "public.aluno_aula ";
                strCommand += "WHERE 1=1 ";
                strCommand += "AND date(public.aluno_aula.data_aula) = @p1 ";
                strCommand += "AND public.aluno_aula.id_aluno = @p2 ";

                using (var command = new NpgsqlCommand(strCommand, connection))
                {
                    command.Parameters.AddWithValue("@p1", data_aula);
                    command.Parameters.AddWithValue("@p2", id_aluno);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
    }
}
