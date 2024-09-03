# XunleiWebUI
| [中文](#中文) | [English](#English) |
|---------------------|-----------------------|

## English
A MVC website tool to add Xunlei download task
Using ASP MVC .Net 4.5 in Win8.1. Debug with VS 2015.

In Debug Mode using VS, it can add a Xunlei download task - magnet or direct link - to the Xunlei software. A magnet link task generates a pop up dialog and need to click the confirm button. This web can do it. The Xunlei app seems to have it contained in a Legacy Chrome Window, which is hard to get its elements. So I locate the dialog by its title and generate a click at the location where a confirm button should be. 

The download task is submitted and generated using ThunderAgent. It's can be imported in "Add Reference - com" if the dev environment have Xunlei installed. I tried to copy the ThunderAgent.dll to another computer, but somehow it doesn't work.

Also, I failed to fetch information from Xunlei with this API. 

In ThunderAgent, here are the APIs I used and worked:
'''
// create agent
ThunderAgentLib.Agent agent = new ThunderAgentLib.Agent();
// add a task, but not commited to the system, it may be possible to add mulitple tasks and commit it once for all
agent.AddTask(magenet, "", "", "", "", 1);
// comment tasks to Xunlei, it would generate the comfirm dialog that normally requires user to click the confirm button
agent.CommitTasks();
'''

The confirm dialog is indentified and the click is generated using user32. All critical code is in windowsUtils.cs file.

Thanks for [xiaotianlan's blog](https://blog.csdn.net/xiaotianlan/article/details/74010511) to explain the details of params of the APIs.

But it somehow failed to send the POST command to IIS when I host it in my Win 8.1 laptop. The error keeps pop up in Console. Wish someone would find how to have it work.

## 中文
本项目是一个基于.Net 4.5的MVC网页工具，本意是用于winnas下的添加迅雷链接下载的网页工具。使用VS2015在Win8.1上编写。
本项目在VS debug中运行正常，可以通过在页面上提交链接（磁力，普通均可）弹出任务确认页面（我打开了快捷下载，但是貌似磁力任务就是会弹窗的）后点击确认按钮将任务加入迅雷中。但我部署在本地IIS中之后会在开始发送任务前就在后台报错，大概率是哪个端口没有正确开放。但愿有哪个大神能觉得这个项目能用拿起调一下吧hhhh

特别鸣谢[xiaotianlan的csdn blog](https://blog.csdn.net/xiaotianlan/article/details/74010511)对ThunderAgent接口的说明。
可惜未能通过接口获取迅雷的任务列表，希望这个项目可以帮助有需要的人减少查资料的工作量。

开始下载按钮的点击是通过user32进行的。所有和ThunderAgent以及user32相关的function都在windowsUtils.cs中。

希望本项目能对windows环境下针对迅雷的二次开发（个人觉得主要需求就是diy winnas的web ui啦）有所帮助。
