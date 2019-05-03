FROM mysql:5.7
ENV MYSQL_DATABASE AppsFlyerTweeter
ENV MYSQL_ALLOW_EMPTY_PASSWORD=1
COPY ./init.sql /docker-entrypoint-initdb.d/