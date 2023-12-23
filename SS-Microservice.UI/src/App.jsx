import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NotFound from './components/NotFound';
import PrivateRoute from './components/PrivateRoute';
import routes from './routes';
import './index.css';
import { HistoryRouter } from './components/HistoryRouter';
import myHistory from './utils/myHistory';
import ScrollToTop from './components/ScrollToTop';
import NotificationContextProvider from './context/NotificationContext';

function App() {

    return (
        <NotificationContextProvider>
            <HistoryRouter history={myHistory}>
                <ScrollToTop />
                <div className="App">
                    <Routes>
                        {routes.map((page, index) => {
                            const loginRequire = page.private;
                            const roles = page.roles;
                            return (
                                <Route
                                    key={index}
                                    path={page.path}
                                    element={
                                        page.layout ? (
                                            loginRequire ? (
                                                <PrivateRoute roles={roles}>
                                                    <page.layout>
                                                        <page.component />
                                                    </page.layout>
                                                </PrivateRoute>
                                            ) : (
                                                <page.layout>
                                                    <page.component />
                                                </page.layout>
                                            )
                                        ) : (
                                            <page.component />
                                        )
                                    }
                                />
                            );
                        })}
                        <Route path="*" element={<NotFound />} />
                    </Routes>
                </div>
            </HistoryRouter>
        </NotificationContextProvider>
    );
}

export default App;
