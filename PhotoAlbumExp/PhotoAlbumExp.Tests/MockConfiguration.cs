using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbumExp.Tests
{
    /// <summary>
    /// Mock configuration class
    /// </summary>
    public class MockConfiguration : IConfiguration
    {
        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ApiUrl { get; set; }

        public MockConfiguration(string apiUrl)
        {
            ApiUrl = apiUrl;
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            return new MockConfigurationSection(key, ApiUrl);
        }
    }
    public class MockConfigurationSection : IConfigurationSection
    {
        private string _key { get; set; }
        private string _value { get; set; }
        public MockConfigurationSection(string key, string value)
        {
            _key = key;
            _value = value;
        }
        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Key => _key;

        public string Path => throw new NotImplementedException();

        public string Value { get { return _value; } set { _value = Value; } }
        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }

}

