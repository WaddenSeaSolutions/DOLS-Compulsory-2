# Build stage
FROM node:20 as build

# Set working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY dols-compulsory-2.client/package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY dols-compulsory-2.client/ ./

# Build the application
# If you need to set the API URL, you can do it during the build command
# For example, if using Angular environment configuration:
# RUN npm run build -- --configuration=production

RUN npm run build

# Production stage
FROM nginx:alpine

# Copy the build output to replace the default nginx contents
COPY --from=build /app/dist/dols-compulsory-2.client /usr/share/nginx/html

# Create a simple script to update configuration at runtime
RUN echo "#!/bin/sh" > /docker-entrypoint.d/40-setup-config.sh && \
    echo "echo 'window.API_URL = \"http://dols-app:8080\";' > /usr/share/nginx/html/config.js" >> /docker-entrypoint.d/40-setup-config.sh && \
    chmod +x /docker-entrypoint.d/40-setup-config.sh

# Expose port 32116
EXPOSE 32116

# Configure nginx to listen on the specified port
RUN sed -i.bak 's/listen\(.*\)80;/listen\1'32116';/' /etc/nginx/conf.d/default.conf

# Start nginx (the default entrypoint will run our script)
CMD ["nginx", "-g", "daemon off;"]
