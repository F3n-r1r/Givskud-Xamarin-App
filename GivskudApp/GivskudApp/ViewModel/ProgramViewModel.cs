using System;
using System.Collections.Generic;
using System.Text;

using GivskudApp.Services;
using GivskudApp.Models;

namespace GivskudApp.ViewModel
{
    public class ProgramViewModel
    {

        ProgramService service = new ProgramService();

        public List<ProgramModel> Program { get { return service.Program; } }

    }
}
