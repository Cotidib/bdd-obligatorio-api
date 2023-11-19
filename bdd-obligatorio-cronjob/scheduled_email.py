#python 3.10.2
# mysql-connector-python 8.2.0

import mysql.connector
import smtplib, ssl

def test():
    print("Ejecutando scheduled email")

def send_emails():
    config = {
    'user': 'bdduser',
    'password': 'bdd1234',
    'host': 'database',
    'database': 'obligatoriodb'
    }

    port = 465  # SSL
    smtp_server = "smtp.gmail.com"
    sender_email = "leonardomilagrosconstanza@gmail.com"
    password = "cfjs tvhg buko bkwp"
    message = """\
    Subject: Obligatorio BdD
    Recordatorio de rellenar el formulario."""

    emails = []

    cnx = mysql.connector.connect(**config)

    if cnx and cnx.is_connected():

        with cnx.cursor() as cursor:

            result = cursor.execute("SELECT Ci, Email from Funcionarios WHERE Ci NOT IN (SELECT Ci from Carnet_Salud)")
            rows = cursor.fetchall()
            print("Buscando emails...")
            for row in rows:
                emails.append(row[1]) #Email

        cnx.close()

    else:

        print("Could not connect")

    cnx.close()

    context = ssl.create_default_context()
    with smtplib.SMTP_SSL(smtp_server, port, context=context) as server:
        server.login(sender_email, password)
        for email in emails:
            print("Email enviado a", email)
            server.sendmail(sender_email, email, message)

if __name__ == "__main__":
  send_emails()