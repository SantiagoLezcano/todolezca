using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using todolezca.Fuctions.Entities;
using System.IO;
using todolezca.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace todolezca.Test.Helpers
{
    class TestFactory
    {
        public static TodoEntity GetTodoEntity()
        {
            return new TodoEntity
            {
                ETag = "*",
                PartitionKey = "TODO",
                RowKey = Guid.NewGuid().ToString(),
                CreatedTime = DateTime.UtcNow,
                IsCompleted = false,
                TaskDescription = "Task: kill the humans."
            };
        }
        public static DefaultHttpRequest CreateHttpRequest(Guid todoId, todo todoRequest)
        {
            string request = JsonConvert.SerializeObject(todoRequest);
            return new DefaultHttpRequest(new DefaultHttpContext())
            {
                Body = GenerateStreamFromString(request),
                Path = $"/{todoId}"
            };
        }

        public static DefaultHttpRequest CreateHttpRequest(Guid todoId)
        {
            return new DefaultHttpRequest(new DefaultHttpContext())
            {
                Path = $"/{todoId}"
            };
        }
        public static DefaultHttpRequest CreateHttpRequest(todo todoRequest)
        {
            string request = JsonConvert.SerializeObject(todoRequest);
            return new DefaultHttpRequest(new DefaultHttpContext())
            {
                Body = GenerateStreamFromString(request)
            };
        }

        public static DefaultHttpRequest CreateHttpRequest()
        {
            return new DefaultHttpRequest(new DefaultHttpContext());
        }

        public static todo GetTodoRequest()
        {
            return new todo
            {
                CreatedTime = DateTime.UtcNow,
                IsCompleted = false,
                TaskDescription = "Try to conquer the world."
            };
         }
        public static Stream GenerateStreamFromString(string StringToConvert)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(StringToConvert);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public static ILogger CreateLogger(LoggerTypes type = LoggerTypes.Null)
        {
            ILogger logger;
            if(type == LoggerTypes.List)
            {
                logger = new ListLogger();
            }
            else
            {
                logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
            }
            return logger;
        }
    }
 }

