import React from 'react';
import ReactDOM from 'react-dom/client';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import App from './App.jsx';
import GlobalStyles from './components/GlobalStyles/GlobalStyles';
import AntdConfigProvider from './components/AntdConfigProvider/index.jsx';
import { GoogleOAuthProvider } from '@react-oauth/google';
import { FloatButton } from 'antd';
import { ArrowUpOutlined, QuestionCircleOutlined } from '@ant-design/icons';

const queryClient = new QueryClient();

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <GoogleOAuthProvider clientId="841642161726-v3mbpdq17eajakh8duatq7kvah65lk83.apps.googleusercontent.com">
            <AntdConfigProvider>
                <QueryClientProvider client={queryClient}>
                    <GlobalStyles>
                        <App />
                        <FloatButton
                            icon={<ArrowUpOutlined />}
                            className="transition-all bg-yellow-200 border-[1px]"
                            onClick={() => window.scrollTo({ top: 0, left: 0, behavior: 'smooth' })}
                        />
                    </GlobalStyles>
                    <ReactQueryDevtools initialIsOpen={false} />
                </QueryClientProvider>
            </AntdConfigProvider>
        </GoogleOAuthProvider>
    </React.StrictMode>,
);
