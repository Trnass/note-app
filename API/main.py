from fastapi import FastAPI, HTTPException, Request
from fastapi.responses import PlainTextResponse
from databases import Database
from pydantic import BaseModel
import datetime
from typing import Union

db_type = "mysql"
username = "semestralka"
password = "Qwert546Lop"
db_ip = "89.203.249.85"
db_name = "school"

DATABASE_URL = f"{db_type}://{username}:{password}@{db_ip}/{db_name}"
database = Database(DATABASE_URL)

tags_metadata = [
    {
        "name": "User Control",
        "description": "Pracuje s uživateli v databázi.",
    },
    {
        "name": "Note Control",
        "description": "Zapisuje poznámky a provádí změny v db.",
    }
]

app = FastAPI(openapi_tags=tags_metadata)

@app.on_event("startup")
async def startup():
    await database.connect()

@app.on_event("shutdown")
async def shutdown():
    await database.disconnect()


class Register(BaseModel):
    username: str
    password: str
    name: str
    surname: str
    mail: str

class Login(BaseModel):
    username: str
    password: str

class NoteAdd(BaseModel):
    username: str
    heading: str
    content: str

class HeadingUpdate(BaseModel):
    id: str
    username: str
    heading: str

class ContentUpdate(BaseModel):
    id: str
    username: str
    content: str

class StatusUpdate(BaseModel):
    id: str
    username: str

class AllNotes(BaseModel):
    username: str

@app.post("/users/register/", tags=["User Control"])
async def register_user(record: Register):
    user_check_query = """
        SELECT * FROM users
        WHERE `username` = :username
    """
    try:
        username_control = await database.fetch_one(user_check_query, values={"username": record.username})
        if username_control:
            raise HTTPException(status_code=501, detail="User with this username has been already created.")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Error with user check: {e}")

    email_control_query = """
        SELECT * FROM users
        WHERE `mail` = :mail
    """
    try:
        email_control = await database.fetch_one(email_control_query, values={"mail": record.mail})
        if email_control:
            raise HTTPException(status_code=501, detail="User with this email has been already created.")
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Error with mail check: {e}")

    query = """
    INSERT INTO `users`(`username`, `password`, `name`, `surname`, `mail`) 
    VALUES(:username, :password, :name, :surname, :mail)
    """
    values = {**record.dict()}
    try:
        await database.execute(query=query, values=values)
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Error inserting user: {e}")

    return {"message": "User registered successfully"}

@app.post("/users/login/", tags=["User Control"])
async def get_stats(record: Login):
    login_check_query = "SELECT * FROM users WHERE username = :username AND password = :password"

    try:
        user_data = await database.fetch_one(login_check_query, values={"username": record.username, "password": record.password})
        
        if user_data:
            return {
                "username": user_data["username"],
                "name": user_data["name"],
                "surname": user_data["surname"],
                "mail": user_data["mail"],
            }
        else:
            raise HTTPException(status_code=404, detail="User not found or wrong credentials")
            
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Error retrieving statistics: {e}")

@app.post("/notes/insert/", tags=["Note Control"])
async def add_note(record: NoteAdd):
    query = """
    INSERT INTO `notes`(`heading`, `content`, `username`) 
    VALUES(:heading, :content, :username)
    """
    values = {**record.dict()}
    try:
        await database.execute(query=query, values=values)
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Error inserting note: {e}")

    return {"message": "Note added successfully"}

@app.patch("/notes/update/heading/", tags=["Note Control"])
async def update_heading(record: HeadingUpdate):
    query = """
    UPDATE `notes`
    SET `heading` = :heading
    WHERE `id` = :id AND `username` = :username
    """
    values = {**record.dict()}
    try:
        await database.execute(query=query, values=values)
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Error updating note: {e}")

    return {"message": "Heading updated successfully"}

@app.patch("/notes/update/content/", tags=["Note cotrol"])
async def update_content(record: ContentUpdate):
    query = """
    UPDATE `notes`
    SET `content` = :content
    WHERE `id` = :id AND `username` = :username
    """
    values = {**record.dict()}
    try:
        await database.execute(query=query, values=values)
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Error updating note: {e}")

    return {"message": "Content updated successfully"}

@app.patch("/notes/update/unfinished/", tags=["Note Control"])
async def update_unfinished(record: StatusUpdate):
    query = """
    UPDATE `notes`
    SET `finished` = 1
    WHERE `id` = :id AND `username` = :username
    """
    values = {**record.dict()}
    try:
        await database.execute(query=query, values=values)
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Error updating note: {e}")

    return {"message": "Finished status unset successfully"}


@app.patch("/notes/update/finished/", tags=["Note Control"])
async def update_finished(record: StatusUpdate):
    query = """
    UPDATE `notes`
    SET `finished` = 2
    WHERE `id` = :id AND `username` = :username
    """
    values = {**record.dict()}
    try:
        await database.execute(query=query, values=values)
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Error updating note: {e}")

    return {"message": "Finished status set successfully"}

@app.patch("/notes/update/priority/", tags=["Note Control"])
async def update_priority(record: StatusUpdate):
    query = """
    UPDATE `notes`
    SET `finished` = 3
    WHERE `id` = :id AND `username` = :username
    """
    values = {**record.dict()}
    try:
        await database.execute(query=query, values=values)
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Error updating note: {e}")

    return {"message": "High priority status set successfully"}

@app.patch("/notes/update/unpriority/", tags=["Note Control"])
async def update_unpriority(record: StatusUpdate):
    query = """
    UPDATE `notes`
    SET `finished` = 1
    WHERE `id` = :id AND `username` = :username
    """
    values = {**record.dict()}
    try:
        await database.execute(query=query, values=values)
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Error updating note: {e}")

    return {"message": "High priority status set successfully"}

@app.get("/notes/all/", tags=["Note Control"])
async def get_all_notes(username: str):
    query = """
    SELECT * FROM `notes`
    WHERE `username` = :username
    """
    values = {'username': username}
    try:
        results = await database.fetch_all(query=query, values=values)
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Error retrieving notes: {e}")

    notes = [{"id": note["id"], "heading": note["heading"], "content": note["content"], "finished": note["finished"], "username": note["username"]} for note in results]

    return {"notes": notes}

@app.post("/notes/delete/", tags=["Note Control"])
async def delete_note(record: StatusUpdate):
    query = """
    DELETE FROM `notes`
    WHERE `id` = :id AND `username` = :username
    """
    values = {**record.dict()}
    try:
        await database.execute(query=query, values=values)
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Error updating note: {e}")

    return {"message": "Note deleted successfully"}
