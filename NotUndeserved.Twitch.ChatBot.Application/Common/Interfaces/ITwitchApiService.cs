using NotUndeserved.Twitch.ChatBot.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotUndeserved.Twitch.ChatBot.Application.Common.Interfaces {
    public interface ITwitchApiService {
        Task<ClipDto> GetClip(string Game, DateTime? From = null, DateTime? To = null);
        Task<ClipDto> GetRandomClip(string Broadcaster);
    }
}
