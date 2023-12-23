#!/bin/bash

docker-compose -f ../compose/docker-compose-infrastructure.yml up -d --build

cd ../

docker-compose up -d --build

docker-compose -f docker-compose-ui.yml up -d --build