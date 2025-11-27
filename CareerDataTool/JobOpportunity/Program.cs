// See https://aka.ms/new-console-template for more information
using CareerDataTool.Data;
using System.Text.Json;

var dataContext = new CareerDataToolDbContext();
Console.WriteLine("Migrar KeyWords?");
if (Console.ReadLine() == "y")
{
    
    var wordRepository = new JobOpportunity.Repository.Word();
    var wordVariationRepository = new JobOpportunity.Repository.WordVariation();

    var wordVariation = wordVariationRepository.GetAll();
    var oldKeyWords = wordRepository.GetAll();
    var oldKeyWordCategories = oldKeyWords
        .GroupBy(w => w.Category)
        .Select(g => g.First())
        .ToList();

    foreach (var oldCategory in oldKeyWordCategories)
    {       
        Console.WriteLine(oldCategory.Category);
        var newCategory = dataContext.KeyWordCategories.Single(c=>c.Name == oldCategory.Category);
        newCategory.KeyWords = new List<CareerDataTool.Domain.Words.KeyWord>();
        var relatedOldKeyWords = oldKeyWords.Where(w => w.Category == oldCategory.Category).ToList();
        foreach (var oldKeyWord in relatedOldKeyWords)
        {
            var newKeyWord = new CareerDataTool.Domain.Words.KeyWord
            {
                Word = oldKeyWord.KeyWord,
                Description = oldKeyWord.Description
            };
            if (wordVariation.Any(w => w.IdWord == oldKeyWord.IdWord))
            {
                newKeyWord.Variations = new List<CareerDataTool.Domain.Words.KeyWordVariation>();
                foreach (var oldVariation in wordVariation.Where(w => w.IdWord == oldKeyWord.IdWord).ToList())
                {
                    var newVariation = new CareerDataTool.Domain.Words.KeyWordVariation
                    {
                        Word = oldVariation.Word
                    };
                    newKeyWord.Variations.Add(newVariation);
                }
            }
            newCategory.KeyWords.Add(newKeyWord);            
        }
        dataContext.SaveChanges();

    }
    
}


Console.WriteLine("Importar palavras para ignorar?");
if (Console.ReadLine() == "y")
{
    List<string> WordsToIgnorePT = new List<string> { };
    List<string> WordsToIgnoreEN = new List<string> { };

    WordsToIgnorePT = ReadJsonStrings("C:\\Users\\ormul\\source\\repos\\jsonWordsToIgnorePT.json").Distinct().ToList();
    WordsToIgnoreEN = ReadJsonStrings("C:\\Users\\ormul\\source\\repos\\jsonWordsToIgnoreEN.json").Distinct().ToList();

    foreach (var word in WordsToIgnorePT)
    {
        var wordToIgnore = new CareerDataTool.Domain.Words.WordToIgnore
        {
            Word = word,
            WordLanguage = CareerDataTool.Domain.Enum.Language.PT_BR
        };
        dataContext.WordsToIgnore.Add(wordToIgnore);
    }
    dataContext.SaveChanges();
    foreach (var word in WordsToIgnoreEN)
    {
        var wordToIgnore = new CareerDataTool.Domain.Words.WordToIgnore
        {
            Word = word,
            WordLanguage = CareerDataTool.Domain.Enum.Language.EN_US
        };
        dataContext.WordsToIgnore.Add(wordToIgnore);
    }
    dataContext.SaveChanges();
}


List<string> ReadJsonStrings(string path)
{
    using (var arquivo = File.OpenRead(path))
    {
        var words = JsonSerializer.Deserialize<List<string>>(arquivo);
        return words;
    }
}