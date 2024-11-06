using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ChamadaKarate.API.Controllers
{

    [Route("api/alunos")]
    [ApiController]
    public class AlunosController : ControllerBase
    {

        [HttpPost("v1/cadastrar")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> Cadastrar([FromBody] core.BLL.Alunos model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var aluno = new core.BLL.Alunos
            {
                nome = model.nome,
                faixa = model.faixa,
                idade = model.idade,
                ativo = model.ativo,
            };

            try
            {
                core.BLL.Alunos alunosInstance = new core.BLL.Alunos();
                int retornoCadastraAluno = await alunosInstance.cadastraAlunoAsync(aluno);

                if (retornoCadastraAluno > 0)
                {
                    return Created("", new Dictionary<string, int> { { "id_aluno", retornoCadastraAluno } });
                }
                else
                {
                    return BadRequest("Ocorreu um erro ao criar o aluno");
                }
            }
            catch
            {
                return StatusCode(500, "Internal Error");
            }
        }

        [HttpGet("v1/listarTodos")]
        public async Task<IActionResult> ListarTodos([FromQuery] int _ativo)
        {

            try
            {
                core.BLL.Alunos alunosInstance = new core.BLL.Alunos();
                List<core.BLL.Alunos> alunos = await alunosInstance.retornaTodosAlunos(_ativo);

                if (alunos != null)
                {
                    //TODO: retornar o id recém criado no banco
                    return Ok(alunos);
                }
                else
                {
                    return BadRequest("Ocorreu um erro ao listar os alunos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Dictionary<string, string> { { "erro", ex.Message } });
            }
        }

        [HttpGet("v1/alunoPorId")]
        public async Task<IActionResult> AlunoPorId([FromQuery] int id)
        {

            try
            {
                core.BLL.Alunos alunosInstance = new core.BLL.Alunos();
                core.BLL.Alunos alunos = await alunosInstance.retornaAlunoPorId(id);

                if (alunos != null)
                {
                    //TODO: retornar o id recém criado no banco
                    return Ok(alunos);
                }
                else
                {
                    return BadRequest("Ocorreu um erro ao retornar o aluno");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Dictionary<string, string> { { "erro", ex.Message } });
            }
        }

        [HttpPut("v1/atualizar")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> Atualizar([FromBody] core.BLL.Alunos model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                core.BLL.Alunos alunosInstance = new core.BLL.Alunos();
                bool retornoAtualizarAluno = await alunosInstance.atualizaAlunoAsync(model);

                if (retornoAtualizarAluno)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Ocorreu um erro ao atualizar o aluno");
                }
            }
            catch
            {
                return StatusCode(500, "Internal Error");
            }
        }
    }
}
