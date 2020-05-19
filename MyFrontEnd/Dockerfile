FROM node as nodeBuilder
COPY . /MyFrontEnd
WORKDIR /MyFrontEnd
RUN npm i
RUN $(npm bin)/ng build

FROM nginx
COPY --from=nodeBuilder /MyFrontEnd/dist/* /usr/share/nginx/html
EXPOSE 80