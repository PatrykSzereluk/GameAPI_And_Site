REGISTER

	https://localhost:44343/Identity/Register

	{
	"Login" : "LOGIN123",
	"Password" : "HASLO122",
	"NickName" : "HASLO1223",
	"Email" : "HASLO1"
	}

LOGIN
	
	https://localhost:44343/Identity/Login

	{
	"Login" : "LOGIN123",
	"Password" : "HASLO122"
	}

GetUserRanking

	https://localhost:44343/Ranking/GetUserRanking

	{
	"Take" : 3,
	"Skip" : 3,
	"RankingCategory" : 1,
	"Order" : false
	}

HOME, TEST AUTH
	
	HEADER : Authorization : Bearer

	https://localhost:44343/Home


ADD CLAN

	https://localhost:44343/Clan/AddNewClan