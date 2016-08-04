# `JSpank.Chat`

JSpank.Chat is a rest chat in php, sqlite, json, angular [JSpank](http://www.jspank.com/)

## Installation
## Usage
	create and add write permission to folder ~/data

* [new chat](#new)
	*~/jspank.new.php?username=your name
	*~/jspank.new.php?userid=00000000-0000-0000-0000-000000000000&username=your_name
* [post to chat](#new)
	*~/jspank.post.php?dbid=00000000-0000-0000-0000-000000000000&username=your_name&post=post message
	*~/jspank.post.php?dbid=00000000-0000-0000-0000-000000000000&username=your_name&post=post message@username_add=your_friend
	*~/jspank.post.php?dbid=00000000-0000-0000-0000-000000000000&username=your_name&post=post message@username_remove=your_friend_to_del
* [get chat]	
	*~/jspank.get.php?dbid=00000000-0000-0000-0000-000000000000&username=your_name
	*~/jspank.get.php?dbid=00000000-0000-0000-0000-000000000000&username=your_name&postid=2
	*~/jspank.get.php?dbid=00000000-0000-0000-0000-000000000000&username=your_name&postid=2&forward=0
	*~/jspank.get.php?dbid=00000000-0000-0000-0000-000000000000&username=your_name&postid=2&forward=1
	