FROM python:3.11.1
RUN python3 -m pip install --upgrade pip
RUN pip install --upgrade python-chess
RUN pip install --upgrade Flask
COPY src /home/app
WORKDIR /home/app
CMD ["flask", "--app", "main", "run", "--host=0.0.0.0", "--port=80"]
