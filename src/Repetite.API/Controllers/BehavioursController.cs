using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Repetite.API.Models;

namespace Repetite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BehavioursController : ControllerBase
    {
        private readonly BehaviourStore _behaviourStore;

        public BehavioursController(BehaviourStore behaviourStore)
        {
            _behaviourStore = behaviourStore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Behaviour>> Get()
        {
            var behaviours = _behaviourStore.All;

            var behaviourModels = behaviours
                .Select(CreateBehaviour);

            return Ok(behaviourModels);
        }

        private Behaviour CreateBehaviour(IBehaviour b)
        {
            return new Behaviour
            {
                Id = b.Id,
                Name = b.Name,
                Inputs = b.Inputs.Select(CreateInput).ToArray(),
                Outputs = b.Outputs.Select(CreateOutput).ToArray()
            };
        }

        private static Models.Output CreateOutput(Output o)
        {
            return new Models.Output {Name = o.Name, Type = o.Type.ToString()};
        }

        private static Models.Input CreateInput(Input i)
        {
            return new Models.Input {Name = i.Name, Type = i.Name};
        }
    }
}