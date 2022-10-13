using Figure.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Figure.API.Controllers {
    [Route("api/figures")]
    [ApiController]
    public class FigureController : ControllerBase {
        private APIResponse _response;

        public FigureController() {
            _response = new();
        }
        
    }
}
