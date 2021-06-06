using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Twi.Models
{
    public class Post
    {
        public static HtmlGenericControl GetContent(string text)
        {
            var content = new HtmlGenericControl("div");
            content.Style.Add("padding", "20px");           

            content.Controls.Add(GetPost(text));

            return content;
        }
        private static Label GetPost(string text)
        {
            Label post = new Label
            {
                Text = text,
            };

            post.Style.Add("font-size", "20px");
            post.Style.Add("font-family", "monospace");

            return post;
        }
        public static HtmlGenericControl GetInfoUser(string avaUrl, Button name)
        {
            var infoUser = new HtmlGenericControl("div");

            infoUser.Style.Add("background-color", "#F0DCF4");
            infoUser.Style.Add("margin-top", "-18.6px");
            infoUser.Style.Add("border-radius", "10px 10px 0 0");

            infoUser.Controls.Add(GetAva(avaUrl));
            infoUser.Controls.Add(name);
            return infoUser;
        }
        private static Image GetAva(string avaUrl)
        {
            var ava = new Image();
            ava.ImageUrl = avaUrl;
            ava.Height = 40;
            ava.Width = 40;
            ava.Style.Add("margin", "4px 0 0 -420px");
            ava.Style.Add("border-radius", "100px");

            return ava;
        }
        public static Button GetName(string name)
        {
            Button nameOfUser = new Button { Text = name, };
            nameOfUser.Style.Add("position", "absolute");
            nameOfUser.Style.Add("background", "inherit");
            nameOfUser.Style.Add("color", "darkmagenta");
            nameOfUser.Style.Add("outline", "none");
            nameOfUser.Style.Add("border", "0");
            nameOfUser.Style.Add("cursor", "pointer");
            nameOfUser.Style.Add("font-size", "20px");
            nameOfUser.Style.Add("font-family", "monospace");
            nameOfUser.Style.Add("left", "84px");
            nameOfUser.Style.Add("top", "12px");

            return nameOfUser;
        }
        public static Button GetBtComm(string id)
        {
            var bt = new Button();
            bt.ID = id;
            bt.Style.Add("background", "inherit");
            bt.Style.Add("color", "darkmagenta");
            bt.Style.Add("outline", "none");
            bt.Style.Add("border", "0");
            bt.Style.Add("cursor", "pointer");
            bt.Style.Add("position", "absolute");
            bt.Style.Add("right", "15px");
            bt.Style.Add("bottom", "6px");
            bt.Text = "Обсудить";

            return bt;
        }

    }
}