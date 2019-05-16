using Api.Services;
using System;
using Xunit;

namespace Tests
{
    public class SwitchServiceTests
    {
        private SwitchService _service;

        public SwitchServiceTests()
        {
            _service = new SwitchService();
        }

        [Fact]
        public void NotImplementedTest()
        {
            Assert.Throws<NotImplementedException>(() => _service.TestMethod());
        }
    }
}
