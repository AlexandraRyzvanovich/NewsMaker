using System;
using EP.Ner;
using EP.Morph;
using EP.Ner.Geo;
using System.Collections.Generic;
using EP.Ner.Org;
using EP.Ner.Person;

namespace WindowsFormsApp1.Utils
{
    class PullentiEntitiesCreator
    {
        private List<string> entitiesList = new List<string>();
        
        public List<Referent> CreateGeoEntities(string text)
        {
            ProcessorService.Initialize(MorphLang.RU | MorphLang.EN);
            GeoAnalyzer.Initialize();
            using (Processor proc = ProcessorService.CreateSpecificProcessor(GeoAnalyzer.ANALYZER_NAME))
            {
                AnalysisResult ar = proc.Process(new SourceOfAnalysis(text));
                var listGeo = new List<Referent>();
                foreach (var e in ar.Entities)
                    if (e is GeoReferent)
                    {
                        listGeo.Add(e);
                        Console.WriteLine(e);
                    }
                return listGeo;
            }
        }

        public List<Referent> CreateOrganizationEntities(string text)
        {
            ProcessorService.Initialize(MorphLang.RU | MorphLang.EN);
            EP.Ner.Org.OrganizationAnalyzer.Initialize();
            using (Processor proc = ProcessorService.CreateSpecificProcessor(OrganizationAnalyzer.ANALYZER_NAME))
            {
                AnalysisResult ar = proc.Process(new SourceOfAnalysis(text));
                var listOrg = new List<Referent>();
                foreach (var e in ar.Entities)
                    if (e is OrganizationReferent)
                    {
                        listOrg.Add(e);
                        Console.WriteLine(e);
                    }
                return listOrg;
            }
        }
        public List<string> CreatePersonEntities(string text)
        {
            ProcessorService.Initialize(MorphLang.RU | MorphLang.EN);
            PersonAnalyzer.Initialize();
            using (Processor proc = ProcessorService.CreateSpecificProcessor(PersonAnalyzer.ANALYZER_NAME))
            {
                AnalysisResult ar = proc.Process(new SourceOfAnalysis(text));
                var listPerson = new List<string>();
                foreach (var e in ar.Entities)
                    if (e is PersonReferent)
                    {
                        var props = e.Slots;
                        var personEntitiName = e.ToString();
                        string properties = null;

                        foreach (var prop in props)
                        {
                            var name = prop.TypeName.ToString();
                            var value = prop.Value.ToString();
                            properties += name += " = " + value + ";";
                        }
                    }
            }
        }

    }
}
