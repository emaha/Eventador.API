using System;
using System.Collections.Generic;
using System.Text;
using Eventador.Domain.Types;

namespace Eventador.Domain.Requests
{
    /// <summary>
    /// Запрос на создание жалобы
    /// </summary>
    public class ComplaintCreateRequest
    {
        public long Id { get; set; }

        public ComplaintType ComplaintType { get; set; }

        public string ReasonText { get; set; }
    }
}
