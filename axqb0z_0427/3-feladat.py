import pymongo as mongo
import pandas as pd

client = mongo.MongoClient("localhost", 27017)

db = client

while True:
    print("----Menu----")
    print("1.: Albumok kilistazasa")
    print("2.: Eloadok kilistazasa")
    print("3.: Uj eloado felvitele")
    print("4.: Album cimenek modositasa")
    print("5.: Album torlese")
    print("6.: Kilepes")

    selected = input(">")

    if(selected == "1"):
        df = pd.DataFrame(db.albums.find())
        print(df)
        
    elif(selected == "2"):
        df = pd.DataFrame(db.artists.find())
        print(df)

    elif(selected == "3"):
        name = input("Eloado neve:")
        country = input("Eloado orszaga:")
        res = db.artists.insert({"name": name, "country": country})
        print(res)

    elif(selected == "4"):
        o_title = input("Adjon meg egy cimet:")
        n_title = input("uj cim:")

        res = db.albums.update_one({"title": o_title}, {"$set": {"title":n_title}})
        print(res.raw_result)

    elif(selected == "5"):
        title = input("Adjon meg egy cimet:")
        res = db.albums.delete_one({"title":title})
        print(res.raw_result)

    elif(selected == "6"):
        break

    else:
        print("Nincs ilyen menupont")

client.close()
