namespace ChatBotPipes.ChatGPTBot;

using ChatBotPipes.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class ChatBotResponse : IChatBotResponse
{
    private readonly object _lockObj = new();

    public event Action<string>? DataReceived;

    private readonly StringBuilder _responseBuilder = new();
    private readonly TaskCompletionSource<string> _tcs = new();

    private readonly SynchronizationContext _eventThreadContext;

    public ChatBotResponse(SynchronizationContext eventThreadContext)
    {
        _eventThreadContext = eventThreadContext;
    }

    public void AppendData(string data)
    {
        lock (_lockObj)
        {
            _responseBuilder.Append(data);
        }

        _eventThreadContext.Post(_ => DataReceived?.Invoke(data), null);
    }

    public void MarkAsCancelled()
    {
        _tcs.SetCanceled();
    }

    public void MarkAsComplete()
    {
        _tcs.SetResult(GetCurrentResponse());
    }

    public string GetCurrentResponse()
    {
        lock (_lockObj)
        {
            return _responseBuilder.ToString();
        }
    }

    public Task<string> AwaitCompletionAsync()
    {
        return _tcs.Task;
    }
}
