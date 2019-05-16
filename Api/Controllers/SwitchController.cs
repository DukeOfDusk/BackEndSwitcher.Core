using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Api.Models;
using Api.Services;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchController : ControllerBase
    {
        private readonly IOptions<BackendSwitcherConfig> _config;
        private ISwitchService _switchService;

        public SwitchController(IOptions<BackendSwitcherConfig> config, ISwitchService switchService)
        {
            _config = config;
            _switchService = switchService;
        }

        [HttpGet]
        [Route("AvailableApplications")]
        public List<Application> AvailableApplications()
        {
            return _config.Value.Applications;
        }

        [HttpGet]
        [Route("AvailableEnvironments")]
        public List<Models.Environment> AvailableEnvironments()
        {
            return _config.Value.Environments;
        }

        [HttpGet]
        [Route("PlaceholdersForApplication/{application}/{environment}")]
        public MatchResult PlaceholdersForApplication([FromRoute] string application, string environment)
        {
            var app = _config.Value.Applications.FirstOrDefault(a => a.Name.ToLower() == application.ToLower());

            if (app != null)
            {
                var templatePlaceholders = _switchService.ParseTemplate(app.TemplatePath);

                if (templatePlaceholders.Count > 0)
                {
                    var env = _config.Value.Environments.FirstOrDefault(e => e.Name.ToLower() == environment.ToLower());

                    if (env != null)
                    {
                        return _switchService.MatchPlaceholders(env.Placeholders, templatePlaceholders);
                    }

                    throw new ArgumentException("Environment not found");
                }
            }

            throw new ArgumentException("Application not found");
        }

        [HttpGet]
        [Route("Transform/{application}/{environment}")]
        public string Transform([FromRoute] string application, string environment)
        {
            var app = _config.Value.Applications.FirstOrDefault(a => a.Name.ToLower() == application.ToLower());

            if (app != null)
            {
                var env = _config.Value.Environments.FirstOrDefault(e => e.Name.ToLower() == environment.ToLower());
                var templatePlaceholders = _switchService.ParseTemplate(app.TemplatePath);

                if (env != null)
                {
                    var matches = _switchService.MatchPlaceholders(env.Placeholders, templatePlaceholders);
                    _switchService.SaveChanges(app.OriginalPath, _switchService.TransformTemplate(app.TemplatePath, matches));
                }
            }

            throw new ArgumentException("wrong");
        }
    }
}
