FROM ubuntu:22.04

ENV DEBIAN_FRONTEND=noninteractive

# ---- Базовые пакеты и локали ----
RUN apt-get update && \
    apt-get install -y \
        locales \
        sudo \
        wget \
        apt-transport-https \
        software-properties-common

# ---- Русская локаль ----
RUN locale-gen ru_RU.UTF-8 en_US.UTF-8 && \
    update-locale LANG=ru_RU.UTF-8 LANGUAGE=ru_RU LC_ALL=ru_RU.UTF-8

ENV LANG=ru_RU.UTF-8
ENV LANGUAGE=ru_RU:ru
ENV LC_ALL=ru_RU.UTF-8

# ---- Создание пользователя ----
RUN useradd -m -s /bin/bash user && \
    echo "user:password" | chpasswd && \
    usermod -aG sudo user

# ---- Confg sudo: требует пароль ----
# По умолчанию sudo спрашивает пароль, но убедимся,
# что нет NOPASSWD правил.
RUN sed -i '/NOPASSWD/d' /etc/sudoers

# ---- Установка .NET SDK ----
RUN wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    rm packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y dotnet-sdk-8.0

# ---- Переключаемся на пользователя ----
USER user
WORKDIR /home/user

CMD ["/bin/bash"]

