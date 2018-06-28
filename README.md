# FrdModBot
Moderation bot for encouraging constructive discussion between different ideological camps

NOTE

You will need to create a file called "config.json" in the FrdModBot directory.

This configures the subreddit to work with and the security details for both the app and the mod account it uses.

Use the following JSON a template:

    {
    	subreddit : "[SUBREDDIT]",
	    client : {
		    id : "[CLIENT ID]",
		    secret : "[CLIENT SECRET]"
	    },
	    user : {
		    username : "[USERNAME]",
		    password : "[PASSWORD]"
	    }
    }
