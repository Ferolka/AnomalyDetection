using AnomalyDetection.Models;
using Common.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalyDetection.Mapperly
{
    [Mapper]
    internal static partial class PredictedPhraseMapperly
    {
        [MapperIgnoreTarget(nameof(Phrase.Id))]
        [MapperIgnoreSource(nameof(PredictedPhrase.Id))]
        [MapperIgnoreSource(nameof(PredictedPhrase.Comment))]
        [MapperIgnoreSource(nameof(PredictedPhrase.Checked))]
        internal static partial Phrase Map(PredictedPhrase source);
    }
}
