#!/bin/bash

docker-compose -f ../compose/docker-compose-infrastructure.yml up -d --build

docker-compose -f ../docker-compose.yml up -d --build 