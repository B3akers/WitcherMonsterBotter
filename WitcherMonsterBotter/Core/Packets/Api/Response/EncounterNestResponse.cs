﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Core.Packets.Api.Response
{
    public class EncounterNestResponse : NestStateResponse
    {
        public override TypeMessage.Method GetMethodId()
        {
            return TypeMessage.Method.EncounterNest;
        }
    }
}
