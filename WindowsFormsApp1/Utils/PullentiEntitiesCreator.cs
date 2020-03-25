using System;
using EP.Ner;
using EP.Morph;
using EP.Ner.Geo;
using System.Collections.Generic;
using EP.Ner.Org;
using EP.Ner.Person;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Utils
{
    class PullentiEntitiesCreator
    {
        public List<Entity> CreateEntities(List<Entity> entities, List<Article> articles)
        {
            foreach (var article in articles)
            {
                var text = article.Text;
                entities = CreateGeoEntities(entities, text);
                entities = CreateOrganizationEntities(entities, text);
                entities = CreatePersonEntities(entities, text);
            }
            return entities;
        }

        private List<Entity> CreateGeoEntities(List<Entity> entities, string text)
        {
            ProcessorService.Initialize(MorphLang.RU | MorphLang.EN);
            GeoAnalyzer.Initialize();
            using (Processor proc = ProcessorService.CreateSpecificProcessor(GeoAnalyzer.ANALYZER_NAME))
            {
                AnalysisResult ar = proc.Process(new SourceOfAnalysis(text));
                foreach (var geo in ar.Entities)
                    if (geo is GeoReferent)
                    {
                        var geoEntitiName = geo.ToString();
                        string entityproperties = null;

                        var props = geo.Slots;
                        string newProperties = null;
                        foreach (var prop in props)
                        {
                            var name = prop.TypeName.ToString();
                            var value = prop.Value.ToString();
                            newProperties += name += " = " + value + ";";
                        }
                        var existingEntity = entities.Find(m => m.Value.Equals(geoEntitiName));
                        if (existingEntity != null)
                        {
                            entityproperties = existingEntity.Properties;
                            if (entityproperties != newProperties)
                            {
                                newProperties += entityproperties;
                            }
                            entities.Remove(existingEntity);
                        }

                        entities.Add(new Entity(geoEntitiName, newProperties, EntitiesType.geo));
                    }
                return entities;
            }
        }

        private List<Entity> CreateOrganizationEntities(List<Entity> entities, string text)
        {
            ProcessorService.Initialize(MorphLang.RU | MorphLang.EN);
            OrganizationAnalyzer.Initialize();
            using (Processor proc = ProcessorService.CreateSpecificProcessor(OrganizationAnalyzer.ANALYZER_NAME))
            {
                AnalysisResult ar = proc.Process(new SourceOfAnalysis(text));
                foreach (var org in ar.Entities)
                    if (org is OrganizationReferent)
                    {
                        var orgEntitiName = org.ToString();
                        string entityproperties = null;

                        var props = org.Slots;
                        string newProperties = null;
                        foreach (var prop in props)
                        {
                            var name = prop.TypeName.ToString();
                            var value = prop.Value.ToString();
                            newProperties += name += " = " + value + ";";
                        }
                        var existingEntity = entities.Find(m => m.Value.Equals(orgEntitiName));
                        if (existingEntity != null)
                        {
                            entityproperties = existingEntity.Properties;
                            if (entityproperties != newProperties)
                            {
                                newProperties += entityproperties;
                            }
                            entities.Remove(existingEntity);
                        }
                        entities.Add(new Entity(orgEntitiName, newProperties, EntitiesType.person));
                    }
                return entities;
            }
        }
        private List<Entity> CreatePersonEntities(List<Entity> entities, string text)
        {
            ProcessorService.Initialize(MorphLang.RU | MorphLang.EN);
            PersonAnalyzer.Initialize();
            using (Processor proc = ProcessorService.CreateSpecificProcessor(PersonAnalyzer.ANALYZER_NAME))
            {
                AnalysisResult ar = proc.Process(new SourceOfAnalysis(text));
                foreach (var e in ar.Entities)
                    if (e is PersonReferent)
                    {
                        var personEntitiName = e.ToString();
                        string entityproperties = null;

                        var props = e.Slots;
                        string newProperties = null;
                        foreach (var prop in props)
                        {
                            var name = prop.TypeName.ToString();
                            var value = prop.Value.ToString();
                            newProperties += name += " = " + value + ";";
                        }
                        var existingEntity = entities.Find(m => m.Value.Equals(personEntitiName));
                        if (existingEntity != null)
                        {
                            entityproperties = existingEntity.Properties;
                            if (entityproperties != newProperties)
                            {
                                newProperties += entityproperties;
                            }
                            entities.Remove(existingEntity);
                        }

                        entities.Add(new Entity(personEntitiName, newProperties, EntitiesType.person));
                    }
                return entities;
            }

        }

    }
}
