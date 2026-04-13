import { $, apiFetch, renderStatus, clearChildren, getQueryParam, setupPagination, setupPageSizeSelect } from '/scripts/common.js';

(async function initActorMoviesIndex() {
	const page = Math.max(1, Number(getQueryParam('page') || localStorage.getItem('am_page') || '1'));
	const size = Math.min(100, Math.max(1, Number(getQueryParam('size') || localStorage.getItem('am_size') || '9')));
	localStorage.setItem('am_page', page);
	localStorage.setItem('am_size', size);
	const listEl = $('#am-list');
	const statusEl = $('#status');
	const tpl = $('#am-card');
	try {
		const payload = await apiFetch(`/actors-movies?page=${page}&size=${size}`);
		const items = Array.isArray(payload) ? payload : (payload.data || []);
		clearChildren(listEl);
		if (items.length === 0) {
			renderStatus(statusEl, 'warn', 'No links found for this page.');
		} else {
			renderStatus(statusEl, '', '');
			for (const am of items) {
				const frag = tpl.content.cloneNode(true);
				const root = frag.querySelector('.card');
				root.querySelector('.actor-id').textContent = `Actor #${am.actorId ?? '—'}`;
				root.querySelector('.movie-id').textContent = `Movie #${am.movieId ?? '—'}`;
				root.querySelector('.link-id').textContent = `Link ID: ${am.id}`;
				root.querySelector('.btn-view').href = `/actor-movie/view.html?id=${encodeURIComponent(am.id)}`;
				root.querySelector('.btn-delete').dataset.id = am.id;
				listEl.appendChild(frag);
			}
		}
		listEl.addEventListener('click', async (ev) => {
			const btn = ev.target.closest('button.btn-delete[data-id]');
			if (!btn) return;
			const id = btn.dataset.id;
			if (!confirm('Remove this actor–movie link? This cannot be undone.')) return;
			try {
				await apiFetch(`/actors-movies/${encodeURIComponent(id)}`, { method: 'DELETE' });
				renderStatus(statusEl, 'ok', `Link ${id} removed.`);
				setTimeout(() => location.reload(), 2000);
			} catch (err) {
				renderStatus(statusEl, 'err', `Delete failed: ${err.message}`);
			}
		});
		setupPageSizeSelect(size);
		setupPagination(payload, page, size);
	} catch (err) {
		renderStatus(statusEl, 'err', `Failed to fetch links: ${err.message}`);
	}
})();
