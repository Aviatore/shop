using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UnitTests
{
    public class MockHttpSession : ISession
    {
        readonly Dictionary<string, byte[]> _sessionStorage = new Dictionary<string, byte[]>();
        string ISession.Id => throw new NotImplementedException();
        bool ISession.IsAvailable => throw new NotImplementedException();
        IEnumerable<string> ISession.Keys => _sessionStorage.Keys;
        void ISession.Clear()
        {
            _sessionStorage.Clear();
        }
        Task ISession.CommitAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        Task ISession.LoadAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        void ISession.Remove(string key)
        {
            _sessionStorage.Remove(key);
        }
        void ISession.Set(string key, byte[] value)
        {
            _sessionStorage[key] = value;
        }
        bool ISession.TryGetValue(string key, out byte[] value)
        {
            if (_sessionStorage.ContainsKey(key))
            {
                value = _sessionStorage[key];
                return true;
            }
            value = null;
            return false;
        }
    }
}