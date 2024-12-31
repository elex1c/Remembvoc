namespace Remembvoc.ApplicationCore.Common.Validation;

public static class Errors
{
    #region Add new word validation errors

    public const string EMPTY_BOXES = "You can't leave text boxes empty";
    public const string WORD_EXISTS = "This word already exists";
    public const string LANGUAGE_NOT_FOUND = "It is not possible to find your language.";

    #endregion

    #region Sentence generation errors
    
    public const string GENERATION_FAILED = "Error while generating a text. Check you Wi-Fi connection and your API Key.";
    
    #endregion
    
    #region WordService

    public const string WORD_NOT_FOUND = "It is impossible to get your word from database. Try delete and add the word again.";
    
    #endregion
    
    #region Translations result messages
    
    public const string INCORRECT_INPUT = "You can't left the input empty";
    public const string INCORRECT_TRANSLATION = "You've typed the wrong translation!";
    
    #endregion

}