﻿namespace ChatBotPipes.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record ChatBotPipeResponse(MappedChatBotTaskTemplate Task, IChatBotResponse Response);
