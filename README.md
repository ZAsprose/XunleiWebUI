# XunleiWebUI
| [中文](#中文) | [English](#english) |
|---------------------|-----------------------|

## english
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

Thanks for [xiaotianlan's blog](https://blog.csdn.net/xiaotianlan/article/details/74010511) to explain the details of params of the APIs.

But it somehow failed to send the POST command to IIS when I host it in my Win 8.1 laptop. Wish someone would find how to have it work.

## 中文
本项目是一个基于.Net 4.5的MVC网页工具，本意是用于winnas下的添加迅雷链接下载的网页工具。
