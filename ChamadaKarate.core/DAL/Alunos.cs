using ChamadaKarate.core.BLL;
using Npgsql;
using System.Configuration;

namespace ChamadaKarate.DAL
{
    public class Alunos
    {
        public async Task<int> cadastraAlunoAsync(core.BLL.Alunos aluno)
        {
            using (var conn = new NpgsqlConnection(core.BLL.Configuracao.getStrConnection("PRD")))
            {
                try
                {
                    conn.Open();
                    string strCommand = "insert into \"alunos\" ( nome, faixa, idade, ativo) values (@p1,@p2,@p3,@p4) RETURNING ID";
                    var command = new NpgsqlCommand(strCommand, conn);
                    command.Parameters.AddWithValue("@p1", aluno.nome.ToUpper());
                    command.Parameters.AddWithValue("@p2", aluno.faixa);
                    command.Parameters.AddWithValue("@p3", aluno.idade);
                    command.Parameters.AddWithValue("@p4", aluno.ativo);
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

        public async Task<List<core.BLL.Alunos>> retornaTodosAlunos(int _ativo)
        {
            var result = new List<core.BLL.Alunos>();

            using (var connection = new NpgsqlConnection(Configuracao.getStrConnection("PRD")))
            {
                connection.Open();

                string strCommand = "";
                strCommand += "SELECT ";
                strCommand += "public.alunos.id "; //0
                strCommand += ", public.alunos.nome "; //1
                strCommand += ", public.alunos.faixa "; //2
                strCommand += ", public.alunos.idade "; //3
                strCommand += ", public.alunos.ativo "; //4
                strCommand += "FROM ";
                strCommand += "public.alunos ";
                strCommand += "WHERE 1=1 ";
                if (_ativo == 1)
                {
                    strCommand += "AND public.alunos.ativo = @p1 ";
                }
                strCommand += "ORDER BY public.alunos.nome ASC ";

                using (var command = new NpgsqlCommand(strCommand, connection))
                {
                    command.Parameters.AddWithValue("@p1", _ativo);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Lê os dados e adiciona à lista
                        while (reader.Read())
                        {
                            var row = new core.BLL.Alunos
                            {
                                id = reader.GetInt32(0),
                                nome = reader.GetString(1),
                                faixa = reader.GetInt32(2),
                                idade = reader.GetInt32(3),
                                ativo = reader.GetInt32(4)
                            };
                            result.Add(row);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<core.BLL.Alunos> retornaAlunoPorId(int id_aluno)
        {
            var result = new core.BLL.Alunos();

            using (var connection = new NpgsqlConnection(Configuracao.getStrConnection("PRD")))
            {
                connection.Open();

                string strCommand = "";
                strCommand += "SELECT ";
                strCommand += "public.alunos.id "; //0
                strCommand += ", public.alunos.nome "; //1
                strCommand += ", public.alunos.faixa "; //2
                strCommand += ", public.alunos.idade "; //3
                strCommand += ", public.alunos.ativo "; //4
                strCommand += "FROM ";
                strCommand += "public.alunos ";
                strCommand += "WHERE 1=1 ";
                strCommand += "AND public.alunos.id = @p1 ";

                using (var command = new NpgsqlCommand(strCommand, connection))
                {
                    command.Parameters.AddWithValue("@p1", id_aluno);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Lê os dados e adiciona à lista
                        while (reader.Read())
                        {
                            var row = new core.BLL.Alunos
                            {
                                id = reader.GetInt32(0),
                                nome = reader.GetString(1),
                                faixa = reader.GetInt32(2),
                                idade = reader.GetInt32(3),
                                ativo = reader.GetInt32(4)
                            };
                            return result = row;
                        }
                    }
                }
            }

            return result;
        }

        public async Task<bool> atualizaAlunoAsync(core.BLL.Alunos aluno)
        {
            using (var conn = new NpgsqlConnection(core.BLL.Configuracao.getStrConnection("PRD")))
            {
                try
                {
                    conn.Open();
                    string strCommand = "update public.alunos set nome = @p1, faixa = @p2, idade = @p3, ativo = @p4 where id = @p0";
                    var command = new NpgsqlCommand(strCommand, conn);
                    command.Parameters.AddWithValue("@p0", aluno.id);
                    command.Parameters.AddWithValue("@p1", aluno.nome);
                    command.Parameters.AddWithValue("@p2", aluno.faixa);
                    command.Parameters.AddWithValue("@p3", aluno.idade);
                    command.Parameters.AddWithValue("@p4", aluno.ativo);
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

    }
}
