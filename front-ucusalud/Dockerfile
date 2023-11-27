FROM node:19.0.0-bullseye-slim as builder

RUN mkdir /frontend
WORKDIR /frontend

RUN npm install -g @angular/cli

COPY package.json package-lock.json ./
RUN npm ci

COPY . .
CMD ["npm", "start"]

FROM builder as dev-envs
WORKDIR /frontend

RUN <<EOF
apt-get update
apt-get install -y --no-install-recommends git
EOF

RUN <<EOF
useradd -s /bin/bash -m vscode
groupadd docker
usermod -aG docker vscode
EOF
# install Docker tools (cli, buildx, compose)
COPY --from=gloursdocker/docker / /

CMD ["npm", "start"]