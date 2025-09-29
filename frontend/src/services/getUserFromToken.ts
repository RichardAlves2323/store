import * as jwt_decode from "jwt-decode";

interface TokenPayload {
  unique_name: string;
  email?: string;
  role?: string;
  iat?: number;
  exp?: number;
}

export function getUserFromToken(): TokenPayload | null {
  const token = localStorage.getItem("token");
  if (!token) return null;

  try {
    const decoded = jwt_decode.jwtDecode<TokenPayload>(token);
    return decoded
  } catch (error) {
    console.error("Erro ao decodificar token", error);
    return null;
  }
}