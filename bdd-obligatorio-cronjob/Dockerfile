FROM alpine:3.16

COPY . .

# Install any dependencies
RUN apk update 
RUN apk upgrade
RUN apk add --update py3-pip
RUN pip install mysql-connector-python

RUN apk add dos2unix

RUN chmod 0655 ./cronjobs
RUN chmod 0655 ./scheduled_email.py

RUN dos2unix ./cronjobs
COPY cronjobs /var/spool/cron/crontabs/root

CMD crond -l 2 -f