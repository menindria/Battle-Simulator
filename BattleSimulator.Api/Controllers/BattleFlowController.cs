using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.CrossCutting;
using Microsoft.AspNetCore.Mvc;

namespace BattleSimulator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleFlowController : ControllerBase
    {
        private readonly IBattleFlowService _battleFlowService;

        public BattleFlowController(
            IBattleFlowService battleFlowService)
        {
            _battleFlowService = battleFlowService;
            
        }

        [HttpPut]
        [Route("Start/{battleId}")]
        public Task<IResponse> Start(int battleId)
        {
            return _battleFlowService.Start(battleId);
        }
        
        [HttpPut]
        [Route("Reset/{battleId}")]

        public Task<IResponse> Reset(int battleId)
        {
            return _battleFlowService.ResetAsync(battleId);
        }
    }
}
