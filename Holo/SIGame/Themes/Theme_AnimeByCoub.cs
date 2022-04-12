﻿using Holo.SIGame;
using Holo.SIGame.Elements;
using Holo.Websites.Website_Coub;
using Holo.Websites.Website_Coub.Structs;
using Holo.Websites.Website_Shikimori;
using Holo.Websites.Website_Shikimori.Structs;

namespace Holo.Themes
{
    class Theme_AnimeByCoub : Theme
    {
        public override void FillQuestion(SIG_question question, Shikimori shiki, string filename_)
        {
            Coub coub = null;
            Anime anime = shiki.GetAnimeByCoub();
            coub = MainCoub.GetAnimeCoub(anime);
            if (coub == null)
            {
                FillQuestion(question, shiki, filename_);
                return;
            }

            question.MediaUrl = coub.FileVersion.File.URL;
            question.MediaPath = Web.GetFilename($"./{filename_}/Video/", ".mp4", out string filename);
            question.MediaFilename = filename;
            question.Answer = $"{anime.Russian}";
        }

        public override void DownloadContent(SIG_question question)
        {
            Web.DownloadFile(question.MediaUrl, question.MediaPath);
        }

        public override string GetXML(SIG_question question)
        {
            string content = "";
            content += $"<question price=\"{question.Price}\">";
            content += SIGamePack.GetQuestionModificator(question);
            content += $"<scenario>";
            content += $"<atom type=\"say\">Ответ - название аниме</atom>";
            content += $"<atom type=\"video\">@{question.MediaFilename}</atom>";
            content += "</scenario><right><answer>";
            content += question.Answer;
            content += "</answer></right></question>";
            return content;
        }

        public override string GetPrettyTitle()
        {
            return "📱 Coub";
        }

        public static string GetRawTitle()
        {
            return "Аниме по coub";
        }
    }
}
