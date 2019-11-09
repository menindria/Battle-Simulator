using System.Collections.Generic;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Commands;
using BattleSimulator.Application.Contracts.Responses;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.CrossCutting;
using Microsoft.AspNetCore.Mvc;

namespace BattleSimulator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleController : ControllerBase
    {
        private readonly IBattleService _battleService;
        private readonly IBattleLogService _battleLogService;

        public BattleController(
            IBattleService battleService,
            IBattleLogService battleLogService)
        {
            _battleService = battleService;
            _battleLogService = battleLogService;
        }

        [HttpPost]
        [Route("Create")]
        public Task<IResponse> Create(CreateBattleCommand command)
        {
            return _battleService.CreateBattleAsync(command);
        }
        
        [HttpGet]
        [Route("GetAll")]
        public Task<IEnumerable<BattleDto>> GetAll()
        {
            return _battleService.GetAllBattlesAsync();
        }
        
        [HttpPost]
        [Route("AddArmyToBattle")]
        public Task<IResponse> AddArmy(AddArmyCommand command)
        {
            return _battleService.AddArmyToBattleAsync(command);
        }
        
        [HttpGet]
        [Route("GetLogs/{battleId}")]
        public Task<IReadOnlyCollection<LogDto>> GetLogs(int battleId)
        {
            return _battleLogService.GetLogs(battleId);
        }
    }
}
