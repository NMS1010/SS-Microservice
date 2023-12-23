import {
    HttpTransportType,
    HubConnectionBuilder,
    HubConnectionState,
    JsonHubProtocol,
    LogLevel,
} from '@microsoft/signalr';

const isDev = process.env.NODE_ENV === 'development';

const startSignalRConnection = async (connection) => {
    try {
        await connection.start();
        console.assert(connection.state === HubConnectionState.Connected);
        console.log('SignalR connection established', connection.baseUrl);
    } catch (err) {
        console.assert(connection.state === HubConnectionState.Disconnected);
        console.error('SignalR Connection Error: ', err);
        setTimeout(() => startSignalRConnection(connection), 5000);
    }
};

const buildConnection = () => {
    const options = {
        logMessageContent: isDev,
        logger: isDev ? LogLevel.Warning : LogLevel.Error,
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
        accessTokenFactory: () => JSON.parse(localStorage.getItem('token'))?.accessToken,
    };
    var connection = new HubConnectionBuilder()
        .withUrl(import.meta.env.VITE_API_URL + '/app-hub', options)
        .withAutomaticReconnect()
        .withHubProtocol(new JsonHubProtocol())
        .configureLogging(LogLevel.Information)
        .build();
    return connection;
};

const getSignalRConnection = async () => {
    let connection = buildConnection();

    connection.onclose(async (error) => {
        if (error) {
            console.log('SignalR: connection was closed due to error.', error);
        } else {
            console.log('SignalR: connection was closed.');
        }
    });
    connection.onreconnecting(async (error) => {
        console.log(error);
    });
    connection.onreconnected((connectionId) => {
        console.log(connectionId);
    });
    await startSignalRConnection(connection);

    return connection;
};

export default getSignalRConnection;
