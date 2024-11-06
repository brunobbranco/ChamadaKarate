using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ChamadaKarate.API.Controllers
{

    [Route("api/aulas")]
    [ApiController]
    public class AulasAlunosController : ControllerBase
    {

        [HttpPost("v1/cadastrar")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> Cadastrar([FromBody] List<core.BLL.AulasAlunosCreateRequest> model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            core.BLL.AulasAlunos retornoCadastraAluno = null;
            foreach (var aluno in model)
            {
                try
                {
                    core.BLL.AulasAlunos alunosInstance = new core.BLL.AulasAlunos();
                    retornoCadastraAluno = await alunosInstance.cadastraAulaAlunoAsync(aluno);

                    if (retornoCadastraAluno == null) continue;
                }
                catch
                {
                    return StatusCode(500, $"Internal Error - Cadastrar aluno {retornoCadastraAluno?.nome_aluno} na aula");
                }
            }
            return Ok("Chamada foi salva com sucesso");
        }

        [HttpGet("v1/listarTodas")]
        public async Task<IActionResult> ListarTodos()
        {

            try
            {
                core.BLL.AulasAlunos aulasInstance = new core.BLL.AulasAlunos();
                List<core.BLL.AulasAlunos> aulas = await aulasInstance.retornaTodosAulas();

                if (aulas != null)
                {
                    //TODO: retornar o id recém criado no banco
                    return Ok(aulas);
                }
                else
                {
                    return BadRequest("Ocorreu um erro ao listar as aulas");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Dictionary<string, string> { { "erro", ex.Message } });
            }
        }

        [HttpGet("v1/listarPorDataAula")]
        public async Task<IActionResult> ListarPorDataAula([FromQuery] string data_aula)
        {
            DateTime _data_aula = Convert.ToDateTime(data_aula);
            try
            {
                core.BLL.AulasAlunos aulasInstance = new core.BLL.AulasAlunos();
                List<core.BLL.AulasAlunos> aulas = await aulasInstance.retornaAulasAlunosPorDataAula(_data_aula);

                if (aulas != null)
                {
                    //TODO: retornar o id recém criado no banco
                    return Ok(aulas);
                }
                else
                {
                    return BadRequest("Ocorreu um erro ao listar a aula");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Dictionary<string, string> { { "erro", ex.Message } });
            }
        }


        [HttpDelete("v1/excluirAlunoAula")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> ExcluirAlunoAula([FromBody] List<core.BLL.AulasAlunosDeleteRequest> model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            foreach(var aluno in model)
            {
                try
                {
                    core.BLL.AulasAlunos alunosInstance = new core.BLL.AulasAlunos();
                    bool retornoAtualizarAluno = await alunosInstance.excluiAulasAlunoAsync(aluno);
                }
                catch
                {
                    return StatusCode(500, "Internal Error - Excluir aluno da aula");
                }
            }
            
            return Ok("Chamada foi salva com sucesso");
        }
    }
}
