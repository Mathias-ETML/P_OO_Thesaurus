/*
 * ETML
 * Clément Sartoni
 * 22.03.2021
 * Projet P_OO-Smart-Thésaurus
 * Fichier contenant tous les enums et les structs qui sont utilisés dans 
 * plusieurs classes du programme.
 */

namespace P_Thesaurus.AppBusiness.EnumsAndStructs
{
    /// <summary>
    /// Enum used to log some messages with a appropriate level.
    /// </summary>
    public enum LogsLevels
    {
        INFO,
        ERROR,
        ALERT,
        DEBUG
    }

    /// <summary>
    /// Enum used to pass some infos form controllers to the main controller
    /// and then to the controller Factory
    /// </summary>
    public enum ControllerType
    {
        Launching,
        Ftp,
        Folder,
        Web
    }

    /// <summary>
    /// Struct used to pass the web infos from model to view
    /// </summary>
    public struct WebElement
    {
        public string link;
        public WebElementType type;
    }

    public enum WebElementType
    {
        Link,
        Image
    }
}
