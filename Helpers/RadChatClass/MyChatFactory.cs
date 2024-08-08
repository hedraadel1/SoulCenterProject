//-----------------------------------------------------------------------
// <copyright file="MyChatFactory.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace SoulCenterProject.Helpers.RadChatClass
{
    public class MyChatFactory : ChatFactory
    {
        public override BaseChatItemElement CreateItemElement(BaseChatDataItem item)
        {
            if(item.GetType() == typeof(TextMessageDataItem))
            {
                return new MyTextMessageItemElement();
            }
            return base.CreateItemElement(item);
        }
    }

    public class MyTextMessageItemElement : TextMessageItemElement
    {
        LightVisualButtonElement likeButton = new LightVisualButtonElement();

        private void likeButton_Click(object sender, EventArgs e)
        {
            if(Data.Tag == null)
            {
                Data.Tag = true;
            } else
            {
                bool isLiked = (bool)Data.Tag;
                Data.Tag = !isLiked;
            }
        }

        protected override SizeF ArrangeOverride(SizeF finalSize)
        {
            SizeF baseSize = base.ArrangeOverride(finalSize);
            RectangleF likeButtonRect;
            RectangleF clientRect = GetClientRectangle(finalSize);
            if(Data.ChatMessagesViewElement.ShowAvatars)
            {
                if(Data.ChatMessagesViewElement.ShowMessagesOnOneSide || !Data.IsOwnMessage)
                {
                    likeButtonRect = new RectangleF(
                        clientRect.X + AvatarPictureElement.DesiredSize.Width + MainMessageElement.DesiredSize.Width,
                        clientRect.Y + NameLabelElement.DesiredSize.Height + MainMessageElement.DesiredSize.Height / 3,
                        likeButton.Image.Width,
                        likeButton.Image.Height);
                } else
                {
                    likeButtonRect = new RectangleF(
                        clientRect.Right -
                            likeButton.DesiredSize.Width -
                            AvatarPictureElement.DesiredSize.Width -
                            MainMessageElement.DesiredSize.Width,
                        clientRect.Y + NameLabelElement.DesiredSize.Height + MainMessageElement.DesiredSize.Height / 3,
                        likeButton.Image.Width,
                        likeButton.Image.Height);
                }
            } else
            {
                if(Data.ChatMessagesViewElement.ShowMessagesOnOneSide || !Data.IsOwnMessage)
                {
                    likeButtonRect = new RectangleF(
                        clientRect.X + MainMessageElement.DesiredSize.Width,
                        clientRect.Y + NameLabelElement.DesiredSize.Height + MainMessageElement.DesiredSize.Height / 3,
                        likeButton.Image.Width,
                        likeButton.Image.Height);
                } else
                {
                    likeButtonRect = new RectangleF(
                        clientRect.Right - likeButton.DesiredSize.Width - MainMessageElement.DesiredSize.Width,
                        clientRect.Y + NameLabelElement.DesiredSize.Height + MainMessageElement.DesiredSize.Height / 3,
                        likeButton.Image.Width,
                        likeButton.Image.Height);
                }
            }
            likeButton.Arrange(likeButtonRect);
            return baseSize;
        }

        protected override void CreateChildElements()
        {
            base.CreateChildElements();
            likeButton.NotifyParentOnMouseInput = true;
            likeButton.Image = Properties.Resources.user;
            likeButton.Size = new Size(30, 30);
            likeButton.Click += likeButton_Click;
            likeButton.EnableElementShadow = false;
            likeButton.Margin = new Padding(10, 0, 10, 0);
            Children.Add(likeButton);
        }

        public override void Synchronize()
        {
            base.Synchronize();
            if(Data.Tag != null && (bool)Data.Tag == true)
            {
                likeButton.Image = Properties.Resources.user;
            } else
            {
                likeButton.Image = Properties.Resources.ai;
            }
        }
    }
}
