FROM node:18-alpine

WORKDIR /app

# Instalar dependencias primero
COPY package.json package-lock.json ./
RUN npm install

# Copiar archivos de configuración
COPY . .

# Puerto e inicio con hot-reload
CMD npm start -- --host 0.0.0.0 --port 4200 --poll 1000
